# ⚙️ GearCalculator v3.0.0

![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![WPF](https://img.shields.io/badge/Framework-WPF-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SolidWorks](https://img.shields.io/badge/Integration-SolidWorks-CE2A23?style=for-the-badge&logo=solidworks&logoColor=white)
![Release](https://img.shields.io/badge/Release-v3.0.0-blue?style=for-the-badge)

**GearCalculator** is a modern, standalone desktop application designed for mechanical engineers, CAD designers, and students. Originally built as a Python script, version 3.0.0 has been completely rewritten in C# and WPF to provide a high-performance, real-time vector rendering engine and an advanced Dark Mode UI.

It calculates precise gear geometries and automatically generates parametric Involute Curve equations for seamless integration with SolidWorks.

## 🚀 What's New in v3.0.0? (The C# & WPF Rewrite)

- **No Python Environment Required:** The tool is now a compiled, standalone `.exe` application. Just download and run!
- **Live Vector Preview:** Instantly renders the gear profile in real-time as you type. Features auto-scaling to fit any dimension perfectly without overflowing.
- **Expanded Gear Types:** Now fully supports **Spur, Helical, Internal, and Bevel gears**.
- **Faux-3D Helical Shadows:** Dynamically generates a 3D-like depth shadow based on the specified Helix Angle (β).
- **Advanced Boolean Geometry:** Visually models exact shaft bores, keyways, and even external pipe boundaries for internal gears by subtracting geometries in real-time.
- **Smart Adaptive UI:** Input fields dynamically hide or show based on the selected gear type (e.g., Shaft details disappear when Internal Gear is selected, replaced by Pipe Outer Diameter).
- **Culture-Independent Inputs:** Safely parses both dots (`.`) and commas (`,`) for decimal values, preventing regional format crashes.

## 📸 Screenshots

Here is the professional v3.0.0 interface showcasing live previews of different gear geometries and calculations.

<p align="center">
  <img src="screenshot/1.jpg" alt="GearCalculator v3.0 Main Interface (English)" width="95%" />
</p>
<br>
<p align="center">
  <img src="screenshot/2.jpg" alt="Live Preview of Helical Gear with Faux-3D Shadow" width="95%" />
</p>
<br>
<p align="center">
  <img src="screenshot/3.jpg" alt="Detailed Internal Gear Modeling with Output Data" width="95%" />
</p>

## 📊 Core Features

* **Instant SolidWorks Equations:** Generates the exact $X(t)$ and $Y(t)$ parametric equations needed to draw a perfect involute tooth profile in SolidWorks.
* **Detailed Engineering Report:** Outputs Pitch, Outer, Root, and Base diameters, alongside the calculated Pitch ($p$).
* **Dual Language Support:** Instantly switch between English and Turkish interfaces.

## 🛠️ Installation & Usage

GearCalculator requires no installation or dependencies.

1. Go to the **[Releases](../../releases)** page.
2. Download the latest `ProGearStudio_v3.0.0.zip` (or `GearCalculator_v3.0.0.zip`).
3. Extract the folder.
4. Run `GearCalculator.exe` and start designing!

## 💻 Tech Stack

- **Language:** C#
- **UI Framework:** WPF (Windows Presentation Foundation)
- **Rendering Engine:** Native WPF `PathGeometry` & `CombinedGeometry` for real-time Boolean operations.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! If you want to add new gear types, export options (like DXF), or CAD macros, feel free to fork the repository and submit a pull request.

---
*Engineered for precision. Built for CAD professionals.*