using System;
using System.Globalization;

namespace GearCalculator
{
    public class GearData
    {
        // Basic Inputs
        public double Module { get; set; }
        public double Teeth { get; set; }
        public double Width { get; set; }
        public double HelixAngle { get; set; }
        public string GearType { get; set; }

        // Shaft and Keyway Details
        public double Bore { get; set; }
        public double KeywayWidth { get; set; }
        public double KeywayDepth { get; set; }

        // Outer pipe diameter for internal gears
        public double PipeDiameter { get; set; }

        // Calculated Diameters
        public double Pitch { get; set; }         // p (Adım) YENİ EKLENDİ
        public double PitchDiameter { get; set; } // d
        public double OuterDiameter { get; set; } // da
        public double RootDiameter { get; set; }  // df
        public double BaseDiameter { get; set; }  // db
        public double BaseRadius { get; set; }    // rb

        // SolidWorks Involute Curve Equations
        public string SwEquationX { get; set; }
        public string SwEquationY { get; set; }
    }

    public class GearEngine
    {
        public static GearData Calculate(double mn, double z, double b, double bore, double kw, double kd, double beta, double pipeDia, string type)
        {
            double d, da, df;
            double betaRad = beta * (Math.PI / 180.0);

            // Helis açısı girilmişse (beta > 0) alın modülünü (mt) hesapla, yoksa normal modülü (mn) kullan.
            double mt = (beta > 0) ? mn / Math.Cos(betaRad) : mn;

            d = mt * z; // Pitch diameter
            double p = Math.PI * mn; // Adım (Pitch) hesaplaması

            // ----------------------------------------------------
            // TIP AND ROOT DIAMETER CALCULATIONS BASED ON GEAR TYPE
            // ----------------------------------------------------
            if (type.Contains("Internal") || type.Contains("İç"))
            {
                // INTERNAL GEAR: Signs are inverted since teeth point towards the center
                da = d - (2 * mn);
                df = d + (2.5 * mn);
            }
            else if (type.Contains("Bevel") || type.Contains("Konik"))
            {
                // BEVEL GEAR
                double delta = 45 * (Math.PI / 180.0);
                da = d + (2 * mn * Math.Cos(delta));
                df = d - (2.5 * mn * Math.Cos(delta));
            }
            else
            {
                // SPUR AND HELICAL GEAR: Standard external gear
                da = d + (2 * mn);
                df = d - (2.5 * mn);
            }

            double db = d * Math.Cos(20 * (Math.PI / 180.0));
            double rb = db / 2.0;

            // ----------------------------------------------------
            // SOLIDWORKS EQUATION ENGINE
            // ----------------------------------------------------
            string rbStr = rb.ToString("0.####", CultureInfo.InvariantCulture);
            string eqX = $"X(t) = {rbStr} * (cos(t) + t * sin(t))";
            string eqY = $"Y(t) = {rbStr} * (sin(t) - t * cos(t))";

            return new GearData
            {
                Module = mn,
                Teeth = z,
                Width = b,
                Bore = bore,
                KeywayWidth = kw,
                KeywayDepth = kd,
                PipeDiameter = pipeDia,
                HelixAngle = beta,
                GearType = type,

                Pitch = p, // Adım (Pitch) pakete eklendi
                PitchDiameter = d,
                OuterDiameter = da,
                RootDiameter = df,
                BaseDiameter = db,
                BaseRadius = rb,

                SwEquationX = eqX,
                SwEquationY = eqY
            };
        }
    }
}