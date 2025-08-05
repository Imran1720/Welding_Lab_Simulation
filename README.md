# 🔧 VR Welding Simulation

A simple VR-based welding simulation built in Unity using **XR Interaction Toolkit** and **XR Device Simulator** (no actual VR headset required). The simulation covers the basic steps of manual welding: toggling gas, adjusting power, grabbing the torch, welding plates, and evaluating weld quality.

> ⚠️ **Deadline:** August 6th, 2025 (End of Day)

---

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
- [ ] Torch is XR Grabbable
- [ ] Trigger starts welding only if:
  - Gas is ON
  - Torch is in valid range
- [ ] Basic particle effect (sparks)

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




