using System;
using System.Linq;
using System.Threading.Tasks;
using Licenses.Authorities.NuGet;
using NSubstitute;
using Xunit;

namespace Licenses.Core.Tests
{
    public class LicenseCheckShould
    {
        private readonly ILicenseCheck _check = new LicenseCheck();

        [Fact]
        public void AddAndRemoveKnownLicenses()
        {
            var license = Substitute.For<License>();
            _check.AddKnownLicenses(Enumerable.Repeat(license, 1));
            Assert.Contains(_check.KnownLicenses, x => x == license);

            _check.RemoveKnownLicense(license);
            Assert.DoesNotContain(_check.KnownLicenses, x => x == license);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenLicenseIsNull()
        {
            Assert.Throws<ArgumentNullException>("licenses", () => _check.AddKnownLicenses(null));
            Assert.Throws<ArgumentNullException>("license", () => _check.AddKnownLicense(null));
            Assert.Throws<ArgumentNullException>("license", () => _check.RemoveKnownLicense(null));
        }

        [Fact]
        public void AddAndRemoveLicenseAuthority()
        {
            var authority = Substitute.For<ILicenseAuthority>();
            _check.AddLicenseAuthority(authority);
            Assert.Contains(_check.LicenseAuthorities, x => x == authority);

            _check.RemoveLicenseAuthority(authority);
            Assert.DoesNotContain(_check.LicenseAuthorities, x => x == authority);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenLicenseAuthorityIsNull()
        {
            Assert.Throws<ArgumentNullException>("licenseAuthority", () => _check.AddLicenseAuthority(null));
            Assert.Throws<ArgumentNullException>("licenseAuthority", () => _check.RemoveLicenseAuthority(null));
        }

        [Fact]
        public async Task ExecuteAsync()
        {
            _check.AddKnownLicense(new License
            {
                Key = "mit",
                Name = "MIT",
                Text = @"MIT License

Copyright (c) [year] [fullname]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files(the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE."
            });

            _check.AddLicenseAuthority(new NuGetLicenseAuthority(_check.KnownLicenses));

            var result = await _check.ExecuteAsync("Flurl.Http.Xml", "1.5.0");
            Assert.NotNull(result);
        }
    }
}
