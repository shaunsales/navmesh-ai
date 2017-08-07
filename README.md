# General Purpose NavMesh/AI for .Net

![Image of Graph](https://dl.dropboxusercontent.com/u/23196369/NavMesh.PNG)

## About
NavMesh/AI is an open source project for both exporting generated NavMesh data, and running pathfinding and agent _AI on the server_. It aims to be a lightweight framework that implements a solid game AI foundation, and is easy for developers to extend for their own needs.

It is written in C# and targets the .Net Core framework for the server component, and Unity 2017.1 and above for the NavMesh & testing components.

## Usage
Generate your NavMesh using Unitys built in Navigation tooling. Then use to GpNavMesh tools to serialize and export. You can also test the graph and path planning in the Unity client, since the libraries are compatible with .Net Standard 1.4.

The path planning and component aims to support common pathfinding algorithms (BF/DF, A\*, D\* Lite), path funneling and smoothing using vector projection and bezier splines. Theres also some vector field generation, but I'm not sure if I'll use it yet. 

The agent component will support basic path traversal and steering via update ticks. There is also a plan to support 3D NavMesh volumes and raycasting. I might try and support RVO (reciprocal velocity obstacles) and dynamic obstables, but there's a lot of more basic functionality to build out first.

## Why build this?
As a game developer, I wanted lightweight AI that ran on the server without needing to load up an entire game engine like Unity. Also being able to control threading means the server load can be regulated by the developer. Having solid pathfinding and agent control on the server opens up options for raycasting and projectile tracing, which is useful shooter games, which is what I make.
