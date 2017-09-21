interface Measurement {
    /**
     * The primary key of the Measurement
     */
    id: number;

    /**
     * The suffix that will be appended to the sales item
     * when this measurement is applied
     */
    suffix: string;

    /**
     * A description of the Measurement
     */
    description?: string;
}
