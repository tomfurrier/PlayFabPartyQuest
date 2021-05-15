// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class OpenSSL : ModuleRules
{
	public OpenSSL(ReadOnlyTargetRules Target) : base(Target)
	{
		Type = ModuleType.External;

		string OpenSSL101sPath = Path.Combine(Target.UEThirdPartySourceDirectory, "OpenSSL", "1_0_1s");
		string OpenSSL111Path = Path.Combine(Target.UEThirdPartySourceDirectory, "OpenSSL", "1.1.1");
		string OpenSSL111dPath = Path.Combine(Target.UEThirdPartySourceDirectory, "OpenSSL", "1.1.1c");
		string OpenSSL111iPath = Path.Combine(Target.UEThirdPartySourceDirectory, "OpenSSL", "1.1.1i");

		string PlatformSubdir = Target.Platform.ToString();
		string ConfigFolder = (Target.Configuration == UnrealTargetConfiguration.Debug && Target.bDebugBuildsActuallyUseDebugCRT) ? "Debug" : "Release";

		if (Target.Platform == UnrealTargetPlatform.Mac || Target.Platform == UnrealTargetPlatform.IOS)
		{
			PublicIncludePaths.Add(Path.Combine(OpenSSL111Path, "Include", PlatformSubdir));

			string LibPath = Path.Combine(OpenSSL111Path, "Lib", PlatformSubdir);

			PublicAdditionalLibraries.Add(Path.Combine(LibPath, "libssl.a"));
			PublicAdditionalLibraries.Add(Path.Combine(LibPath, "libcrypto.a"));
		}
		else if (Target.Platform == UnrealTargetPlatform.PS4)
		{
			string IncludePath = Target.UEThirdPartySourceDirectory + "OpenSSL/1.0.2g" + "/" + "include/PS4";
			string LibraryPath = Target.UEThirdPartySourceDirectory + "OpenSSL/1.0.2g" + "/" + "lib/PS4/release";
			PublicIncludePaths.Add(IncludePath);
			PublicAdditionalLibraries.Add(LibraryPath + "/" + "libssl.a");
			PublicAdditionalLibraries.Add(LibraryPath + "/" + "libcrypto.a");
		}
		else if (Target.Platform == UnrealTargetPlatform.Win64 || Target.Platform == UnrealTargetPlatform.Win32 ||
				Target.Platform == UnrealTargetPlatform.HoloLens)
		{
			// Our OpenSSL 1.1.1 libraries are built with zlib compression support
			PrivateDependencyModuleNames.Add("zlib");

			string VSVersion = "VS" + Target.WindowsPlatform.GetVisualStudioCompilerVersionName();

			// Add includes
			PublicIncludePaths.Add(Path.Combine(OpenSSL111Path, "include", PlatformSubdir, VSVersion));

			// Add Libs
			string LibPath = Path.Combine(OpenSSL111Path, "lib", PlatformSubdir, VSVersion, ConfigFolder);

			PublicAdditionalLibraries.Add(Path.Combine(LibPath, "libssl.lib"));
			PublicAdditionalLibraries.Add(Path.Combine(LibPath, "libcrypto.lib"));
			PublicSystemLibraries.Add("crypt32.lib");
		}
		else if (Target.IsInPlatformGroup(UnrealPlatformGroup.Unix))
		{
			string platform = "/Linux/" + Target.Architecture;
			string IncludePath = OpenSSL111dPath + "/include" + platform;
			string LibraryPath = OpenSSL111dPath + "/lib" + platform;

			PublicIncludePaths.Add(IncludePath);
			PublicAdditionalLibraries.Add(LibraryPath + "/libssl.a");
			PublicAdditionalLibraries.Add(LibraryPath + "/libcrypto.a");

			PublicDependencyModuleNames.Add("zlib");
//			PublicAdditionalLibraries.Add("z");
		}
		else if (Target.Platform == UnrealTargetPlatform.Android || Target.Platform == UnrealTargetPlatform.Lumin)
		{
			string platform = "/Android";
			string IncludePath = OpenSSL111iPath + "/include" + platform;
			string LibraryPath = OpenSSL111iPath + "/lib" + platform;

			PublicIncludePaths.Add(IncludePath);
			PublicAdditionalLibraries.Add(LibraryPath + "/libssl.a");
			PublicAdditionalLibraries.Add(LibraryPath + "/libcrypto.a");

			PublicDependencyModuleNames.Add("zlib");
			// string OpenSSL111bPath = Path.Combine(Target.UEThirdPartySourceDirectory, "OpenSSL", "1.1.1b");
			// string IncludePath = OpenSSL111bPath + "/include/Android";
			// string LibraryPath = OpenSSL111bPath + "/lib/Android";
			//
			// PublicIncludePaths.Add(IncludePath);
			//
			// // unneeded since included in libcurl
			// string LibPath = Path.Combine(OpenSSL111dPath, "lib", PlatformSubdir);
			// //PublicLibraryPaths.Add(LibPath);
			// //PublicAdditionalLibraries.Add(LibraryPath + "/libssl.a");
			// //PublicAdditionalLibraries.Add(LibraryPath + "/libcrypto.a");
		}
	}
}
