; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
MIK0001 | Mikita.Generation | Error | The implementation manifest is unreadable
MIK0002 | Mikita.Generation | Error | The implementation manifest is invalid
MIK0003 | Mikita.Generation | Error | Several implementation manifests are present
MIK0004 | Mikita.Generation | Error | The implementation type is unknown
MIK0005 | Mikita.Generation | Error | The implementation type is declared in another assembly
MIK0006 | Mikita.Generation | Error | The implementation type is declared twice
MIK0007 | Mikita.Generation | Error | The live implementation requires an interface
MIK0008 | Mikita.Generation | Error | The live implementation cannot relay a member
MIK0009 | Mikita.Generation | Error | The live relay registry is missing
MIK0010 | Mikita.Generation | Error | Module initializers are unavailable