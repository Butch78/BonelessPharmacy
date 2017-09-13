enum OrderStatus {
    In_progress,
    Complete
}

interface Order 
{
    /**
    * The primary key of the Order
    */
    id: number;

    /**
     * The time the order is created
     */
    createdAt: string

    /**
     * The Order status
     */
    OrderStatus: OrderStatus
}