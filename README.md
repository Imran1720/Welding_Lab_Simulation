# 🔧 VR Welding Simulation

### Candidate Name: 
Shaik Imran Ali<br>
### Brief Description:<br>
A simple VR-based welding simulation built in Unity using **XR Interaction Toolkit** and **XR Device Simulator** (no actual VR headset required). The simulation covers the basic steps of manual welding: toggling gas, adjusting power, grabbing the torch and simple welding plates.

---
## Demo

[![Watch the video](https://img.youtube.com/vi/j0jTQfTYOkw/maxresdefault.jpg)](https://youtu.be/j0jTQfTYOkw)

## External Assests

 <a href="https://assetstore.unity.com/packages/3d/props/tdg-storage-solutions-252198" target="_blank">TDG Storage Solutions </a>

## Project Approach Summary

For this welding simulation, I focused on creating an interactive VR welding system using Unity and the XR Interaction Toolkit.
The system is built around two main components:

<ol>
  <li>
  <b>WeldHandler</b> – Controls welding logic, listens to VR controller input (<b>InputActionProperty</b>), and handles the start/stop of the welding process, including activating particle effects and aligning the weld drag point during the operation.
  </li>
  <li>
   <b>WeldZoneHandler</b> – Manages the weldable area, dynamically positions and scales the weld mesh (small or big) based on the user’s current power level and the distance between the start and drag points.
  </li>
</ol>
<b>The welding flow is event-driven:</b>
<ul>
  <li>
    <b>Gas On</b> and <b>Power Level</b> changes are handled via a central <b>WeldSimulationService</b> event system.
  </li>
  <li>
    On entering a weld zone (<b>OnTriggerEnter</b>), the system caches the drag point and enables welding visuals.
  </li>
  <li>
    When the primary weld button is pressed, particle effects start; releasing the button stops them.
  </li>
  <li>
    The weld mesh continuously updates based on the drag point’s movement.
  </li>  
</ul>

<b>Development Challenge</b>
Due to the absence of a <b>VR headset</b> for testing, I relied entirely on the XR Device Simulator in Unity for development. While this allowed for basic interaction testing, some trigger events (especially physics-based collider triggers in VR hand controllers) could not be fully verified in real headset conditions. This means certain input or collider behaviors may need adjustment once tested on the actual target hardware.


## 📌 Core Features Checklist

Each functionality below will be implemented in a **separate branch**. Once completed and tested, it will be checked off here.

### ✅ System: XR Setup
- [x] XR Origin prefab setup (Camera, Hands, Input Actions)
- [x] XR Device Simulator integrated and working
- [x] Interaction Profiles created

---

### ✅ System: Environment Setup
- [x] Metal table with two metal plates positioned
- [x] Player spawn point & workspace area

---

### ✅ System: Gas Cylinder
- [x] Interactable gas cylinder with ON/OFF toggle
- [x] Visual indicator for gas flow status

---

### ✅ System: Power Control (Current Output)
- [x] Interactable knob or slider
- [x] Map value to welding power (0–100%)
- [x] Display current value on UI

---

### ✅ System: Welding Torch
- [x] Torch is XR Grabbable
- [x] Trigger starts welding only if:
  - Gas is ON
  - Torch is in valid range
- [x] Basic particle effect (sparks)

---

### ✅ System: Movement Tracking
- [ ] Track torch tip movement speed in real-time
- [ ] Calculate speed over time (e.g., last 0.5s window)

---

### ✅ System: Weld Quality Evaluation
- [ ] Evaluate weld quality based on:
  - Speed consistency
  - Power setting
  - Contact with weld joint
- [ ] Output result (Good, Weak, Overburnt)

---

### ✅ System: UI
- [ ] Display current power and gas status
- [ ] Show weld feedback after welding
- [ ] Simple placeholder UI (canvas or world-space)

---

### ✅ System: Folder & Code Architecture
- [x] Project folder structured properly
- [x] Scene created and named
- [x] Prefabs, Scripts, and Assets organized

---


### 🛠️ Tech Stack
- Unity 2022.3 LTS
- XR Interaction Toolkit
- XR Device Simulator
- C# / Unity Scripting

---

## 🗂️ Project Folder Structure

```plaintext
Assets/
├── Art/
│   ├── Models/
│   ├── Materials/
│   └── Textures/
├── Audio/
│   └── SFX/
├── Prefabs/
│   ├── WeldingTorch.prefab
│   ├── MetalPlate.prefab
│   ├── GasCylinder.prefab
│   └── UIElements.prefab
├── Scenes/
│   └── MainScene.unity
├── Scripts/
│   ├── Core/
│   ├── Welding/
│   ├── Interaction/
│   ├── UI/
│   └── Utils/
├── XR/
│   ├── XR Origin.prefab
│   └── XR Device Simulator.asset
└── Resources/




