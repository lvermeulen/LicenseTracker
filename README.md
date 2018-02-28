![Icon](https://i.imgur.com/ds930kG.png?1)
# LicenseTracker
[![Build status](https://ci.appveyor.com/api/projects/status/u8uftiej6f58axnc?svg=true)](https://ci.appveyor.com/project/lvermeulen/licensetracker)
 [![license](https://img.shields.io/github/license/lvermeulen/LicenseTracker.svg?maxAge=2592000)](https://github.com/lvermeulen/LicenseTracker/blob/master/LICENSE) [![NuGet](https://img.shields.io/nuget/v/LicenseTracker.svg?maxAge=86400)](https://www.nuget.org/packages/Licenses.Core/) ![](https://img.shields.io/badge/.net-4.5-yellowgreen.svg) ![](https://img.shields.io/badge/netstandard-1.4-yellowgreen.svg)

LicenseTracker is a library for tracking licenses of dependencies.

## Features:
* License sources: GitHub and Spdx
* License authorities: NuGet and npm
* License URL providers: NuGet
* License readers: GitHub
* Write your own license source, authority, url provider and reader

## Usage:

* Get licenses from GitHub:
~~~~
	var licenseSource = new GitHubLicenseSource();
	var licenses = await licenseSource.GetLicensesAsync();
~~~~

* Get licenses from Spdx:
~~~~
	var licenseSource = new SpdxLicenseSource();
	var licenses = await licenseSource.GetLicensesAsync();
~~~~

* Check license of dependency:
~~~~
	var check = new LicenseCheck();

	var licenses = await licenseSource.GetLicensesAsync();
	check.AddKnownLicenses(licenses);

	check.AddLicenseAuthority(new NuGetLicenseAuthority(check.KnownLicenses));
	check.AddLicenseAuthority(new NpmLicenseAuthority(check.KnownLicenses));

	var license = await check.ExecuteAsync("Newtonsoft.Json", "11.0.1");
~~~~

## Thanks
* [License](https://thenounproject.com/term/license/1546827/) icon by [beth bolton](https://thenounproject.com/bethbolton/) from [The Noun Project](https://thenounproject.com)
