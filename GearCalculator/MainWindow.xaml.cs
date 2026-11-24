using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace GearCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void BtnMaximize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        private void BtnClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private double SafeParse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            string cleanStr = input.Replace(',', '.');
            double.TryParse(cleanStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double result);
            return result;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UIElement element = e.Source as UIElement;
                element?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void cmbGearType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            int idx = cmbGearType.SelectedIndex;

            // Düz dişli (idx=0) hariç Helisel(1), İç(2) ve Konik(3) için Helis Açısı kutusunu açar.
            bool needsHelix = (idx == 1 || idx == 2 || idx == 3);
            if (panelHelix != null) panelHelix.Visibility = needsHelix ? Visibility.Visible : Visibility.Collapsed;
            if (!needsHelix && txtHelixAngle != null) txtHelixAngle.Clear();

            if (panelPipe != null) panelPipe.Visibility = (idx == 2) ? Visibility.Visible : Visibility.Collapsed;
            if (panelShaft != null) panelShaft.Visibility = (idx == 2) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void cmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            bool isTR = cmbLanguage.SelectedIndex == 1;

            lblMainTitle.Text = isTR ? "DİŞLİ TASARIMCISI" : "GEAR GENERATOR";
            lblGearType.Text = isTR ? "Dişli Tipi Seçimi" : "Gear Type Selection";
            lblBasicParams.Text = isTR ? "TEMEL PARAMETRELER" : "BASIC PARAMETERS";
            lblModule.Text = isTR ? "Modül" : "Module";
            lblTeeth.Text = isTR ? "Diş Sayısı" : "Teeth Count";
            if (lblHelixAngle != null) lblHelixAngle.Text = isTR ? "Helis Açısı (β)°" : "Helix Angle (β)°";
            lblWidth.Text = isTR ? "Genişlik (B) mm" : "Face Width (B) mm";

            lblPipeDetails.Text = isTR ? "DIŞ ÇEPER DETAYLARI" : "EXTERNAL PIPE DETAILS";
            lblPipeDia.Text = isTR ? "Boru Dış Çapı (Ø)" : "Pipe Outer Diameter (Ø)";

            lblShaftDetails.Text = isTR ? "MİL VE KAMA DETAYLARI" : "SHAFT & KEYWAY DETAILS";
            lblBore.Text = isTR ? "Mil Deliği Çapı (Ø)" : "Bore Diameter (Ø)";
            lblKeyWidth.Text = isTR ? "Kama Genişliği" : "Keyway Width";
            lblKeyDepth.Text = isTR ? "Kama Derinliği" : "Keyway Depth";

            cbiGearSpur.Content = isTR ? "Düz Dişli" : "Spur Gear";
            cbiGearHelical.Content = isTR ? "Helisel Dişli" : "Helical Gear";
            cbiGearInternal.Content = isTR ? "İç Dişli" : "Internal Gear";
            cbiGearBevel.Content = isTR ? "Konik Dişli" : "Bevel Gear";

            btnGenerate.Content = isTR ? "HESAPLA VE ÇİZ" : "GENERATE & DRAW";
            txtTechData.Text = isTR ? "--- HESAPLAMA SONUÇLARI ---\nSonuçlar bekleniyor..." : "--- CALCULATION RESULTS ---\nWaiting for results...";
            lblPreview.Text = isTR ? "CANLI ÖN İZLEME" : "LIVE PREVIEW";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtModule.Clear(); txtTeeth.Clear();
            if (txtHelixAngle != null) txtHelixAngle.Clear();
            txtWidth.Clear(); txtPipeDia.Clear(); txtBore.Clear(); txtKeyWidth.Clear(); txtKeyDepth.Clear();
            GearCanvas.Children.Clear();
            txtTechData.Text = cmbLanguage.SelectedIndex == 1 ? "--- HESAPLAMA SONUÇLARI ---\nEkran temizlendi." : "--- CALCULATION RESULTS ---\nScreen cleared.";
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double m = SafeParse(txtModule.Text);
                double z = SafeParse(txtTeeth.Text);
                double b = SafeParse(txtWidth.Text);
                double bore = SafeParse(txtBore.Text);
                double kw = SafeParse(txtKeyWidth.Text);
                double kd = SafeParse(txtKeyDepth.Text);
                double pipeDia = SafeParse(txtPipeDia.Text);
                double beta = panelHelix.Visibility == Visibility.Visible ? SafeParse(txtHelixAngle.Text) : 0;

                string type = (cmbGearType.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Spur Gear";

                GearData result = GearEngine.Calculate(m, z, b, bore, kw, kd, beta, pipeDia, type);

                bool isTR = cmbLanguage.SelectedIndex == 1;
                string report = isTR ? "=== GELİŞMİŞ TASARIM RAPORU ===\n\n[ GİRİLEN DEĞERLER ]\n" : "=== ADVANCED DESIGN REPORT ===\n\n[ INPUT VALUES ]\n";

                if (isTR)
                {
                    report += $"Modül (m): {result.Module}  |  Diş Sayısı (z): {result.Teeth}\n";
                    report += $"Adım (p): {result.Pitch:F2} mm\n"; // Adım eklendi
                    if (result.HelixAngle > 0) report += $"Helis Açısı (β): {result.HelixAngle}°\n";
                    if (result.Width > 0) report += $"Genişlik (B): {result.Width} mm\n";
                    if (result.PipeDiameter > 0) report += $"Dış Boru Çapı: {result.PipeDiameter} mm\n";
                    if (result.Bore > 0) report += $"Mil Deliği (Ø): {result.Bore} mm  |  Kama: {result.KeywayWidth}x{result.KeywayDepth} mm\n";

                    report += "\n[ HESAPLANAN ÇAPLAR ]\n";
                    report += $"Bölüm Dairesi Çapı (d) : {result.PitchDiameter:F2} mm\n";
                    report += $"Dış Çap (da)           : {result.OuterDiameter:F2} mm\n";
                    report += $"Taban Çapı (df)        : {result.RootDiameter:F2} mm\n";
                    report += $"Temel Daire Çapı (db)  : {result.BaseDiameter:F2} mm\n\n";

                    report += "[ SOLIDWORKS EVOLVENT DENKLEMLERİ ]\n";
                    report += $"{result.SwEquationX}\n{result.SwEquationY}\n";
                }
                else
                {
                    report += $"Module (m): {result.Module}  |  Teeth (z): {result.Teeth}\n";
                    report += $"Pitch (p): {result.Pitch:F2} mm\n"; // Pitch eklendi
                    if (result.HelixAngle > 0) report += $"Helix Angle (β): {result.HelixAngle}°\n";
                    if (result.Width > 0) report += $"Width (B): {result.Width} mm\n";
                    if (result.PipeDiameter > 0) report += $"Pipe Outer Dia: {result.PipeDiameter} mm\n";
                    if (result.Bore > 0) report += $"Bore Dia (Ø): {result.Bore} mm  |  Keyway: {result.KeywayWidth}x{result.KeywayDepth} mm\n";

                    report += "\n[ CALCULATED DIAMETERS ]\n";
                    report += $"Pitch Dia (d) : {result.PitchDiameter:F2} mm\n";
                    report += $"Outer Dia (da): {result.OuterDiameter:F2} mm\n";
                    report += $"Root Dia (df) : {result.RootDiameter:F2} mm\n";
                    report += $"Base Dia (db) : {result.BaseDiameter:F2} mm\n\n";

                    report += "[ SOLIDWORKS INVOLUTE EQUATIONS ]\n";
                    report += $"{result.SwEquationX}\n{result.SwEquationY}\n";
                }
                txtTechData.Text = report;

                DrawGear(result);
            }
            catch (Exception)
            {
                MessageBox.Show(cmbLanguage.SelectedIndex == 1 ? "Lütfen geçerli sayılar girin!" : "Please enter valid numbers!", "Hata / Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DrawGear(GearData gear)
        {
            GearCanvas.Children.Clear();
            double centerX = GearCanvas.ActualWidth / 2.0;
            double centerY = GearCanvas.ActualHeight / 2.0;
            if (centerX == 0 || centerY == 0) return;

            bool isInternal = gear.GearType.Contains("Internal") || gear.GearType.Contains("İç");

            bool isTypeHelical = gear.GearType.Contains("Helical") || gear.GearType.Contains("Helisel") || gear.GearType.Contains("Spiral") || gear.GearType.Contains("Bevel") || gear.GearType.Contains("Konik");
            bool isHelical = isTypeHelical && gear.HelixAngle > 0;

            double pipeDia = SafeParse(txtPipeDia.Text);

            double maxDim = gear.OuterDiameter;
            if (isInternal) maxDim = pipeDia > gear.RootDiameter ? pipeDia : gear.RootDiameter + 20;
            if (maxDim <= 0) maxDim = 100;

            double scale = (Math.Min(GearCanvas.ActualWidth, GearCanvas.ActualHeight) * 0.85) / maxDim;

            if (isHelical)
            {
                DrawShape(gear, centerX, centerY, scale, true, isInternal, pipeDia, gear.HelixAngle);
            }

            DrawShape(gear, centerX, centerY, scale, false, isInternal, pipeDia, gear.HelixAngle);

            Line hLine = new Line { X1 = centerX - 15, Y1 = centerY, X2 = centerX + 15, Y2 = centerY, Stroke = Brushes.Red, StrokeThickness = 1 };
            Line vLine = new Line { X1 = centerX, Y1 = centerY - 15, X2 = centerX, Y2 = centerY + 15, Stroke = Brushes.Red, StrokeThickness = 1 };
            GearCanvas.Children.Add(hLine);
            GearCanvas.Children.Add(vLine);
        }

        private void DrawShape(GearData gear, double cx, double cy, double scale, bool isShadow, bool isInternal, double pipeDia, double beta)
        {
            int z = (int)Math.Floor(gear.Teeth);
            if (z < 3) return;

            double angleStep = 2 * Math.PI / z;
            double rRoot = (gear.RootDiameter / 2.0) * scale;
            double rTip = (gear.OuterDiameter / 2.0) * scale;

            PathGeometry profile = new PathGeometry();
            PathFigure fig = new PathFigure { IsClosed = true };

            double rotationOffset = isShadow ? (beta * 0.005) : 0;
            double offsetX = isShadow ? 4 : 0;
            double offsetY = isShadow ? 4 : 0;

            for (int i = 0; i < z; i++)
            {
                double a = (i * angleStep) + rotationOffset;
                double rootAngle1 = a - (angleStep * 0.25);
                double tipAngle1 = a - (angleStep * 0.10);
                double tipAngle2 = a + (angleStep * 0.10);
                double rootAngle2 = a + (angleStep * 0.25);

                Point p1 = new Point(cx + offsetX + rRoot * Math.Cos(rootAngle1), cy + offsetY + rRoot * Math.Sin(rootAngle1));
                Point p2 = new Point(cx + offsetX + rTip * Math.Cos(tipAngle1), cy + offsetY + rTip * Math.Sin(tipAngle1));
                Point p3 = new Point(cx + offsetX + rTip * Math.Cos(tipAngle2), cy + offsetY + rTip * Math.Sin(tipAngle2));
                Point p4 = new Point(cx + offsetX + rRoot * Math.Cos(rootAngle2), cy + offsetY + rRoot * Math.Sin(rootAngle2));

                if (i == 0) fig.StartPoint = p1;
                else fig.Segments.Add(new LineSegment(p1, true));

                fig.Segments.Add(new LineSegment(p2, true));
                fig.Segments.Add(new LineSegment(p3, true));
                fig.Segments.Add(new LineSegment(p4, true));
            }
            profile.Figures.Add(fig);

            Geometry finalShape = profile;

            if (isInternal)
            {
                double rPipe = (pipeDia / 2.0) * scale;
                if (rPipe <= rRoot) rPipe = rRoot + (10 * scale);

                Geometry pipeGeo = new EllipseGeometry(new Point(cx + offsetX, cy + offsetY), rPipe, rPipe);
                finalShape = new CombinedGeometry(GeometryCombineMode.Exclude, pipeGeo, profile);
            }
            else if (gear.Bore > 0 && !isShadow)
            {
                double rBore = (gear.Bore / 2.0) * scale;
                Geometry boreGeo = new EllipseGeometry(new Point(cx, cy), rBore, rBore);

                if (gear.KeywayWidth > 0 && gear.KeywayDepth > 0)
                {
                    double kw = gear.KeywayWidth * scale;
                    double kd = gear.KeywayDepth * scale;
                    Rect kwRect = new Rect(cx - (kw / 2.0), cy - rBore - kd, kw, kd + 5);
                    boreGeo = new CombinedGeometry(GeometryCombineMode.Union, boreGeo, new RectangleGeometry(kwRect));
                }
                finalShape = new CombinedGeometry(GeometryCombineMode.Exclude, profile, boreGeo);
            }

            Path path = new Path { Data = finalShape };

            if (isShadow)
            {
                path.Fill = new SolidColorBrush(Color.FromArgb(120, 0, 0, 0));
            }
            else
            {
                path.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
                path.Stroke = Brushes.White;
                path.StrokeThickness = 1.5;
                path.Effect = new DropShadowEffect { Color = Colors.Black, BlurRadius = 15, ShadowDepth = 5, Opacity = 0.6 };
            }

            GearCanvas.Children.Add(path);
        }
    }
}