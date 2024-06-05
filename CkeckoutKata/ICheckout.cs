namespace CheckoutKata
{
    interface ICheckout
    {
        /// <summary>
        /// Scan an item into the current basket
        /// </summary>
        /// <param name="item">The barcode of the item</param>
        void Scan(string item);

        /// <summary>
        /// Get the total price of all items
        /// </summary>
        /// <returns>Price as an int</returns>
        int GetTotalPrice();
    }
}
