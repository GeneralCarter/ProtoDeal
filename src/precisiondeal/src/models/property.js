var formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
});

class Property {
    constructor(dto) {
        this.dto = dto;
    }

    get id() {
        return this.dto.id;
    }

    get propertyName() {
        return this.dto.propertyName;
    }

    get price() {
        return formatter.format(this.dto.price);
    }
}

export default Property;

