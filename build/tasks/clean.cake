#load ../data/*.cake


Task("clean/artifacts/pkg").Does(() => CleanDirectory(paths.Packages));
Task("clean/artifacts/lib").Does(() => CleanDirectory(paths.Libraries));
