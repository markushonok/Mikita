#!/usr/bin/env bash

set -euo pipefail

root="$(git rev-parse --show-toplevel)"

exec "$root/Modules/Markushonok Methodology/Programming/TrimEof.sh" \
	'*.cs' \
	'*.csproj' \
	'*.gitignore' \
	'*.md' \
	'*.props' \
	'*.sh' \
	'*.sln'