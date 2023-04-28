#tool "dotnet:?package=GitVersion.Tool&version=5.12"

static GitVersion version;
version = GitVersion();
