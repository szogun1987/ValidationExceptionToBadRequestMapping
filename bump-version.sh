#!/bin/bash
read current_version
IFS='.' read -a version_parts <<< $current_version
major=${version_parts[0]}
minor=${version_parts[1]}
patch=${version_parts[2]}
patch=$((patch + 1))
echo $major.$minor.$patch