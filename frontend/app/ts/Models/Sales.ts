interface Sale {
    /**
     * The Id/Key of the Sale
     */
    id?: number;

    /**
     * The time the sale was created
     */
    createdAt: string;

    /**
     * The contents of the sale
     */
    contents?: SalesRecord[];
}
