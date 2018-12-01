namespace SupermarketCheckout
{
    interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
