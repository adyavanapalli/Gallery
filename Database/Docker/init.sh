#!/usr/bin/env bash

set -o errexit
set -o nounset
set -o pipefail
set -o posix
set -o xtrace

sudo docker run --detach \
                --env POSTGRES_USER="$USER" \
                --env POSTGRES_PASSWORD=postgres \
                --publish 127.0.0.1:5432:5432 \
                postgres

sleep 10s

PGPASSWORD=postgres psql --host=localhost --file="$(find . -name images.sql)"
