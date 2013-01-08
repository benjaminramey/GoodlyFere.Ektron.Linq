properties { 
  $base_dir  = resolve-path ..\
  $build_base_dir = "$base_dir\build"
  $build_dir = "$build_base_dir\build"
  $packageinfo_dir = "$base_dir"
  $debug_build_dir = "$build_dir\bin\debug"
  $release_build_dir = "$build_dir\bin\release"
  $release_dir = "$build_base_dir\Release"
  $sln_file = "$base_dir\GoodlyFere.Ektron.Linq.sln"
  $version = "1.0.0"
  $revision = ""
  $tools_dir = "$build_base_dir\Tools"
  $run_tests = $true
  $xunit_console = "$tools_dir\xunit.console.clr4.exe"
  $version_info_file = "$base_dir\GoodlyFere.Ektron.Linq\Properties\VersionInfo.cs"
}

Framework "4.0"

task default -depends Package

task Clean {
  if (Test-Path $build_dir) { remove-item -force -recurse $build_dir }
  if (Test-Path $release_dir) { remove-item -force -recurse $release_dir }
}

task Init -depends Clean {
	mkdir @($release_dir, $build_dir) | out-null
	
    #UpdateVersion
}

task Compile -depends Init {
  Exec { msbuild $sln_file /p:"OutDir=$debug_build_dir\;Configuration=Debug;TargetFrameworkVersion=v4.0" } "msbuild (debug) failed."
  Exec { msbuild $sln_file /p:"OutDir=$release_build_dir\;Configuration=Release;TargetFrameworkVersion=v4.0" } "msbuild (release) failed."
}

task Test -depends Compile -precondition { return $run_tests }{
  cd $debug_build_dir
  Exec { & $xunit_console "GoodlyFere.Ektron.Linq.Tests.dll" } "xunit failed."
}

task Package -depends Compile, Test {
  $spec_files = @(Get-ChildItem $packageinfo_dir "*.nuspec" -Recurse)

  foreach ($spec in @($spec_files))
  {
	$dir =  $($spec.Directory)
	cd $dir
    Exec { nuget pack -o $release_dir -Properties Configuration=Release`;OutDir=$release_build_dir\ -Symbols } "nuget pack failed."
  }
}

Function UpdateVersion {
	# assembly version
	$assemblyVersion = gc $version_info_file | select-string "AssemblyVersion\(""(\d+\.\d+\.\d+\.\d+)""\)" | %{$_.Matches[0].Groups[1].Value}
	$parts = $assemblyVersion.Split('.')
	$major = $parts[0]
	$minor = $parts[1]
	$build = [int]$parts[2] + 1
	$rev = $parts[3]
	# file version
	$fileVersion = gc $version_info_file | select-string "AssemblyFileVersion\(""(.+)""\)" | %{$_.Matches[0].Groups[1].Value}
	$text = "using System.Reflection;

[assembly: AssemblyVersion(""$major.$minor.$build.$rev"")]
[assembly: AssemblyFileVersion(""$fileVersion"")]
"
	Write-Output $text > $version_info_file
}
