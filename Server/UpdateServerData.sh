#!/bin/bash

set -e
sudo su
cd ygoserver/ygopro
rm -rf ygopro-FogMoe-card-database
git clone -b without-pics https://api.mtr.pub/scarletkc/ygopro-FogMoe-card-database.git
cp -rf ygopro-FogMoe-card-database/* ./ && rm -r ygopro-FogMoe-card-database
mv FogMoe-cards.cdb cards.cdb

echo "Update complete!"
