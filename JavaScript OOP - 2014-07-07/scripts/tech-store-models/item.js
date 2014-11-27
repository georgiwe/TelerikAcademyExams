/// <reference path="../libs/require.js" />

define(function () {

    var Item = (function () {

        var ITEM_CONSTANTS = {
            minNameLength: 6,
            maxNameLength: 40,
            invalidNameLengthErrorMessage: 'Item name cannot be shorter than 6 characters, or longer than 30.',
            invalidItemTypeErrorMessage: 'Invalid item type.',
            invalidItemPriceErrorMessage: 'Item price cannot be negative.',
            itemTypes: {
                accessory: 'accessory',
                smartPhone: 'smart-phone',
                notebook: 'notebook',
                pc: 'pc',
                tablet: 'tablet'
            },
        };

        function Item(type, name, price) {
            validateItemType(type);
            validateItemName(name);
            validateItemPrice(price);

            this.type = type;
            this.name = name;
            this.price = price;
        }

        Item.prototype.clone = function () {
            return {
                type: this.type,
                name: this.name,
                price: this.price
            };
        }

        function validateItemName(name) {
            var nameLength = name.length;
            if (nameLength < ITEM_CONSTANTS.minNameLength ||
                    nameLength > ITEM_CONSTANTS.maxNameLength) {
                throw {
                    message: ITEM_CONSTANTS.invalidNameLengthErrorMessage
                };
            }
        }

        function validateItemType(type) {
            if (type !== ITEM_CONSTANTS.itemTypes.accessory &&
                    type !== ITEM_CONSTANTS.itemTypes.smartPhone &&
                    type !== ITEM_CONSTANTS.itemTypes.notebook &&
                    type !== ITEM_CONSTANTS.itemTypes.pc &&
                    type !== ITEM_CONSTANTS.itemTypes.tablet) {
                throw {
                    message: ITEM_CONSTANTS.invalidItemTypeErrorMessage
                };
            }
        }

        function validateItemPrice(price) {
            if (price < 0) {
                throw {
                    message: ITEM_CONSTANTS.invalidItemPriceErrorMessage
                };
            }
        }

        return Item;
    })();

    return Item;
});