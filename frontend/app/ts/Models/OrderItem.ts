interface OrderItem
{
    /**
    * The primary key of the Order Item
    */
    id: number;
    
    /**
     * The id of the Order item Order
     * orderId: number;
     */

    /**
     * The related Order object
     * order: Order;
     */

     /**
     * The id of the Order item Supplier
     * supplierCodeID: number;
     */

    /**
     * The related Supplier object
     * supplier: Supplier;
     */
    
    /**
    * Quantity of order
    */
    quantity: number;
    
    /**
    * The price of order
    */
    price: number;
}