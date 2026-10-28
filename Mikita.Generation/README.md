# Mikita.Generation

Source generators for Mikita.

## Implementations manifest

Each project that declares types needing generation keeps an `Implementations.toml` next to its project file and includes it as an additional file:

```xml
<ItemGroup>
	<AdditionalFiles Include="Implementations.toml" />
</ItemGroup>
```

```toml
version = 1

[[implementation]]
type = "Mikita.IO.Paths.IPath"
features = ["live"]
```

Rules:

* `version` is required and is currently `1`.
* A project may have one manifest. Several manifests are an error.
* `type` is a C# type name, including constructed generics such as `System.Collections.Generic.IEnumerable<My.Type>`.
* A project may declare only the types it declares itself. A type from another assembly must be declared in that assembly's manifest. A constructed generic is allowed when at least one of its type arguments belongs to the declaring project; that project then owns the relay for the construction.
* Unknown keys, unknown features, repeated keys, and repeated types are errors.
* A string cannot contain `"` or `\`. Only `#` line comments and single-line arrays are supported. Anything else is reported as an invalid manifest.

## `live` feature

Generates a relay class that implements the declared interface and forwards every member to `Func<T>`, then registers it in `Mikita.Evaluation.Liveness.LiveRelay<T>.Factory` from a module initializer. `Mikita.Evaluation.Liveness.Live.ResultOf<T>` uses only the registered relay and throws when the declaring project has no entry for the type.

Requirements and limits:

* The type must be an interface declared in the current assembly.
* Static interface members, `init`-only setters, ref returns, and pointer or function-pointer signatures are reported as unsupported.
* `ResultOf<T>` throws `NotSupportedException` when no relay is registered. Every type passed to `Live.ResultOf`, `LiveValue`, or `Live` must therefore be declared in a manifest.

## Diagnostics

`MIK0001`–`MIK0010` are build errors. Their identifiers and titles are listed in `AnalyzerReleases.Unshipped.md`.

## Referencing

As a package, it ships only `analyzers/dotnet/cs`, so a plain `PackageReference` is enough. As a project reference, use `OutputItemType="Analyzer"` and `ReferenceOutputAssembly="false"`.