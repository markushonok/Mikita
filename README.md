# Mikita

**A general-purpose .NET toolkit for application code.**

Mikita brings together reusable types for observing changes, coordinating work, handling data, and accessing files. Networking, Godot integration, and source generation are available as separate packages.

## Packages

| Package | Use it for |
| --- | --- |
| [`Mikita`](Mikita/) | Core abstractions and utilities. |
| [`Mikita.Network`](Mikita.Network/README.md) | Peer connections, correlated requests and responses, and one-way notices over LiteNetLib. |
| [`Mikita.Godot`](Mikita.Godot/) | Godot integration for nodes, deferred execution, input, and resource access. |
| [`Mikita.Generation`](Mikita.Generation/README.md) | A C# source generator for interface relays declared in `Implementations.toml`. |

## Explore the core

- [Observation](Mikita/Observation/): events, subscriptions, and observable values.
- [Threading](Mikita/Threading/): task pools, cancellation, and task adapters.
- [Data structures](Mikita/Structs/): references, optional values, collections, and binary buffers.
- [File and folder access](Mikita/IO/): paths, files, folders, and file system abstractions.
- [Math](Mikita/Math/): vectors, matrices, ranges, sizes, and bounding boxes.

## Get started

Add the core package to a .NET 10 project:

```bash
dotnet add package Mikita --prerelease
```

For a runnable networking example, follow the [Mikita.Network quickstart](Mikita.Network/README.md). To generate interface relays, see the [Mikita.Generation guide](Mikita.Generation/README.md).

## Build from source

Use the .NET 10 SDK. Clone with submodules to get the shared code style and engineering documentation.

```bash
git clone --recurse-submodules https://github.com/markushonok/Mikita.git
cd Mikita
dotnet build Mikita.slnx
dotnet test Mikita.slnx --no-build
```