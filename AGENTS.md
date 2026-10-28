# Engineering Guidelines

Before modifying the codebase, read all files matching `Modules/Markushonok Methodology/*/AGENTS.md`.

The referenced guidelines are normative and apply to all project code unless explicitly overridden by this file.

# Code Style

JetBrains Rider applies `Modules/Markushonok Code Style/Markushonok.DotSettings` through the injected layer declared in `Mikita.sln.DotSettings`. The layer is team-shared and portable: Rider resolves it through the stored relative path, so it requires the `Modules/Markushonok Code Style` submodule to be initialized. Do not move the layer into a personal profile or `.DotSettings.user`.

`.editorconfig` declares the indentation that the layer expects: tabs, size 2. Rider's editor reads indentation from there, not from the settings layer, so keep both in sync.