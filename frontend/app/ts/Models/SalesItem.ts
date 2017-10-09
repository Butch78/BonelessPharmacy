interface SalesItem {
    /**
     * The primary key of the SalesItem
     */
    id: number;

    /**
     * The name of the item
     * @example Panadol Paracetamol
     */
    name: string;

    /**
     * The supplier ID number for the product
     */
    supplierCode: string;

    /**
     * The price of the product, GST excluded
     * @example 23.60
     */
    price: number;

    /**
     * The stock of this item in store
     * @example 16
     */
    stockOnHand: number;

    /**
     * How much individual parts of the item are in a single sale of it.
     * This can be a measurement of count, volume, size etc...
     */
    amount: number;

    /**
     * The id of the sales item measurement
     */
    measurementId: number;

    /**
     * The related measurement object
     */
    measurement: Measurement;

    /**
     * Probs don't need this
     * 0 = No, 1 = Yes
     */
    isArchived: number;
}
