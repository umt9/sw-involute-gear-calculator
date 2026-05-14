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

<img width="1822" height="1294" alt="3" src="https://github.com/user-attachments/assets/4d9d6b81-8d80-455d-91f8-523bb91b3acd" />
<img width="1817" height="1287" alt="2" src="https://github.com/user-attachments/assets/d0fe9afb-51c0-4037-92c4-5335ad583d2c" />
<img width="1813" height="1283" alt="1" src="https://github.com/user-attachments/assets/8ca0144f-bebb-42ec-80b9-8e5e129e52bd" />


## 📊 Core Features

* **Instant SolidWorks Equations:** Generates the exact $X(t)$ and $Y(t)$ parametric equations needed to draw a perfect involute tooth profile in SolidWorks.
* **Detailed Engineering Report:** Outputs Pitch, Outer, Root, and Base diameters, alongside the calculated Pitch ($p$).
* **Dual Language Support:** Instantly switch between English and Turkish interfaces.

## 🛠️ Installation & Usage

GearCalculator requires no installation or dependencies.

1. Go to the **[Releases](../../releases)** page.
2. Download the latest `GearCalculator_v3.0.0.exe`.
3. Run `GearCalculator.exe` and start designing!

## 💻 Tech Stack

- **Language:** C#
- **UI Framework:** WPF (Windows Presentation Foundation)
- **Rendering Engine:** Native WPF `PathGeometry` & `CombinedGeometry` for real-time Boolean operations.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! If you want to add new gear types, export options (like DXF), or CAD macros, feel free to fork the repository and submit a pull request.

---
*Engineered for precision. Built for CAD professionals.*
