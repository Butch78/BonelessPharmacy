interface SalesRecord
{
    id: number;

    saleId: number;

    sale: Sale;

    itemId: number;

    salesItem: SalesItem;

    quantity: number;
}