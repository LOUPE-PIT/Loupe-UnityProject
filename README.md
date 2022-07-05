# Loupe-UnityProject

## Contents

[TOC]

## Introduction

The Practorate Interactive Technology of ROC Tilburg (PIT)[^1], built an Augmented Reality (AR) application with which teachers can support their lessons via the Microsoft HoloLens 2. The AR app, called Loupe, makes it possible for teachers and students to collaborate simultaneously in separate locations with custom 3D models in the context of practice-oriented education. Of course, the teachers and students can also do this in the same room. Teachers can import their own 3D models (created themselves, scanned themselves, or purchased via the internet) into the app to make them useable for their education. The applications are wide; it can be used for both practical and theoretical lessons. As an example, consider learning the different components of an engine block or organ. It can be used in almost all majors.

## How to open the project

1. Download and install [Unity Hub](https://unity3d.com/get-unity/download).

2. Install Unity version **2020.3.28f1** using the Unity Hub with the *Universal Windows Platform Build Support *and *Windows Build Support (IL2CPP)* modules: <img src="https://docs.microsoft.com/en-us/windows/mixed-reality/develop/images/unity_install_option_uwp.png" alt="Unity Hub" style="zoom: 67%;" />

3. **Opening the project**: Unfortunately, cloning the repo will cause errors. This is because necessary modifications have been made to a package *(MRTK Toolkit)*, which will otherwise be freshly imported into the project via the package manager when you open the Unity Project. Instead, download and extract the master branch in [this zip file](https://mega.nz/file/5lhX2D5S#BQpRrhTQya5r2ICyqi9jnTvOPn1oF9B0Zzdy8Vm7aGU), and locate the extracted folder as a repo. From there on, you should be good to go. Open the project using Unity Hub.

   

## How to deploy the project to the HoloLens

Read *[Build and deploy to the HoloLens](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/build-and-deploy-to-hololens).* Note that in some cases, wireless deployment via WiFi doesn't work when you're using Eduroam WiFi. In that case, use a hotspot or USB cable instead.

## Recommended reading

If you're going to be developing this project further, it is recommended to understand the basics of the techniques that have been used to develop this HoloLens application. It is recommended to follow these articles from top to bottom (and follow them along in a clean Unity Project).

1. [What is Mixed Reality?](https://docs.microsoft.com/en-us/windows/mixed-reality/discover/mixed-reality)
2. [Introduction to the Mixed Reality Toolkit-Set Up Your Project and Use Hand Interaction](https://docs.microsoft.com/en-us/learn/modules/learn-mrtk-tutorials/)
3. [Place a Mars Rover object in the scene and work with grids and intelligent object tracking](https://docs.microsoft.com/en-us/learn/modules/place-scene-objects/)
4. [Getting started with 3D object interaction](https://docs.microsoft.com/en-us/learn/modules/get-started-with-object-interaction/)
5. For the multi-user capabilities: [Multi-user capabilities tutorials](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/tutorials/mr-learning-sharing-01) (all of them).
6. <u>This tutorial</u>, made for teachers who want to import their own 3D models for usage in the app.

## Other useful resources

- The [Trello workspace](https://trello.com/invite/pitloupe/67b741f09ae7b03bc8c735ab7a7fd754) in which you can create your Trello board.

- The [MEGA folder](https://mega.nz/fm/R1xjSARQ) of the complete archive of project related files made by the previous parties who have worked on this project.

  - The login credentials are available on the the backlog of the [*"LOUPE Back-end"* Trello board](https://trello.com/invite/b/RDldlSvD/d8eacfb31fe860fc04242fa5c6e73b60/loupe-back-end) (see the *"Reading/viewing existing BitWarden"* card).

- [Mixed Reality documentation](https://docs.microsoft.com/en-us/windows/mixed-reality/)

- [Photon Unity Networking Documentation](https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro)

  

[^1]: Practorate is derived from "Lectorate". Lectorate is a Dutch word for a research group within Universities (of Applied Sciences). A Practorate is the same, but practorates conduct research in the Dutch secondary vocational education (MBO) system.

