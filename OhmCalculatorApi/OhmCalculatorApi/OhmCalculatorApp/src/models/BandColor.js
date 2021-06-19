class BandColor {
    constructor(color, value) {
        if (!color) {
            throw 'Color should be specified';
        }

        this.color = color;
        this.value = value ?? 0;
    }
}