#!/usr/bin/env bash

set -euo pipefail

root="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source="https://api.nuget.org/v3/index.json"

projects=(
	"Mikita/Mikita.csproj"
	"Mikita.Godot/Mikita.Godot.csproj"
)

fail()
{
	printf 'Error: %s\n' "$1" >&2
	exit 1
}

[[ -n "${NUGET_API_KEY:-}" ]] || \
	fail "NUGET_API_KEY is not set. Create an API key on nuget.org and export it."

for project in "${projects[@]}"; do
	printf 'Packing %s\n' "$project"
	dotnet pack "$root/$project" -c Release
done

find "$root" \( -name '*.nupkg' -o -name '*.snupkg' \) -path '*/bin/Release/*' -print0 \
	| while IFS= read -r -d '' package; do
		printf 'Pushing %s\n' "$package"
		dotnet nuget push "$package" \
			--api-key "$NUGET_API_KEY" \
			--source "$source" \
			--skip-duplicate
	done
