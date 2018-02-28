namespace Licenses.Core
{
    public interface ILicenseComparer
    {
        bool LicensesEqual(string left, string right);
    }
}
