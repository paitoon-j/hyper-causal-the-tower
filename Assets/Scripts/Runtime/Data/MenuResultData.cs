public class MenuResultData
{
    private int _menuPrice = 1;
    private int _addonPrice = 0;
    private int _premiumAddonPrice = 0;
    private int _multiplier = 2;

    public int MenuPrice
    {
        get { return this._menuPrice; }
        set { this._menuPrice = value; }
    }

    public int AddonPrice
    {
        get { return this._addonPrice; }
        set { this._addonPrice = value; }
    }

    public int PremiumAddonPrice
    {
        get { return this._premiumAddonPrice; }
        set { this._premiumAddonPrice = value; }
    }

    public int Total
    {
        get {
            int total = this._menuPrice * (this._addonPrice + this._premiumAddonPrice);
            return total; 
        }
    }

    public int Multiplier
    {
        get
        {
            int multiplier = this._multiplier;
            return multiplier;
        }
    }
}
