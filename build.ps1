msbuild /m /verbosity:detailed /p:Configuration=Debug /p:Platform=x64 ./vs/G2DataGUI.sln
if(!$?) {
    throw "Failed to build G2DataGUI Debug"
}

msbuild /m /verbosity:detailed /p:Configuration=Release /p:Platform=x64 ./vs/G2DataGUI.sln
if(!$?) {
    throw "Failed to build G2DataGUI Release"
}