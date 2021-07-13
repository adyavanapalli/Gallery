#!/usr/bin/env bash

set -o errexit
set -o nounset
set -o pipefail
set -o posix
set -o xtrace

dotnet publish --configuration=Release -p:PublishProfile=Default
