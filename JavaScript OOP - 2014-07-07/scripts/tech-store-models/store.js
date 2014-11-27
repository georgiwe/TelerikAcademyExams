/// <reference path="../libs/require.js" />

define(['tech-store-models/item'], function (Item) {

    var Store = (function () {

        var STORE_CONSTANTS = {
            minNameLength: 6,
            maxNameLength: 30,
            invalidNameErrorMessage: 'Store name length must be between 6 and 30 characters.',
            invalidItemTypeErrorMessage: 'Invalid item.',
            itemTypes: {
                accessory: 'accessory',
                smartPhone: 'smart-phone',
                notebook: 'notebook',
                pc: 'pc',
                tablet: 'tablet'
            },
        };

        function Store(storeName) {
            validateStoreName(storeName);

            this._name = storeName;
            this._items = [];
        }

        // result in all methods (except filtered by price) 
        // is sorted because it gets sorted in getAll()
        Store.prototype.addItem = function (item) {
            validateItem(item);
            this._items.push(item);
            return this;
        };

        Store.prototype.getAll = function (filter, performSortByName) {
            var itemsClone = [],
                currItem,
                len,
                i;

            filter = filter || function () {
                return true;
            };

            if (performSortByName === undefined) {
                performSortByName = true;
            }

            for (i = 0, len = this._items.length; i < len; i++) {
                currItem = this._items[i];

                if (filter(currItem)) {
                    itemsClone.push(currItem.clone());
                }
            }

            if (performSortByName) {
                itemsClone.sort(sortByName);
            }

            return itemsClone;
        };

        Store.prototype.getSmartPhones = function () {
            var result = this.getAll(function (currItem) {
                return currItem.type === 'smart-phone';
            });

            return result;
        };

        Store.prototype.getMobiles = function () {
            var result = this.getAll(function (currItem) {
                return currItem.type === 'smart-phone' ||
                       currItem.type === 'tablet';
            });

            return result;
        };

        Store.prototype.getComputers = function () {
            var result = this.getAll(function (currItem) {
                return currItem.type === 'pc' ||
                       currItem.type === 'notebook';
            });
            return result;
        }

        Store.prototype.filterItemsByType = function (itemType, performSortByName) {
            var result = this.getAll(function (currItem) {
                return currItem.type === itemType;
            }, performSortByName);
            return result;
        }

        Store.prototype.filterItemsByPrice = function (options) {
            options = {
                min: (options && options.min) || 0,
                max: (options && options.max) || Number.MAX_VALUE
            };

            var result = this.getAll(function (currItem) {
                return currItem.price >= options.min &&
                       currItem.price <= options.max;
            }, false);

            result.sort(sortByPrice);
            return result;
        };

        Store.prototype.filterItemsByName = function (name) {
            name = name.toLowerCase();
            var result = this.getAll(function (currItem) {
                return currItem.name.toLowerCase().indexOf(name) !== -1;
            });
            return result;
        };

        Store.prototype.countItemsByType = function () {
            var result = [];

            result[STORE_CONSTANTS.itemTypes.accessory] =
                this.filterItemsByType(STORE_CONSTANTS.itemTypes.accessory, false).length;
            result[STORE_CONSTANTS.itemTypes.smartPhone] =
                this.filterItemsByType(STORE_CONSTANTS.itemTypes.smartPhone, false).length;
            result[STORE_CONSTANTS.itemTypes.notebook] =
                this.filterItemsByType(STORE_CONSTANTS.itemTypes.notebook, false).length;
            result[STORE_CONSTANTS.itemTypes.pc] =
                this.filterItemsByType(STORE_CONSTANTS.itemTypes.pc, false).length;
            result[STORE_CONSTANTS.itemTypes.tablet] =
                this.filterItemsByType(STORE_CONSTANTS.itemTypes.tablet, false).length;

            return result;
        };

        function sortByName(item1, item2) {
            var name1 = item1.name.toLowerCase();
            var name2 = item2.name.toLowerCase();

            if (name1 < name2) {
                return -1;
            } else if (name1 === name2) {
                return 0;
            } else {
                return 1;
            }
        }

        function sortByPrice(item1, item2) {
            return item1.price - item2.price;
        }

        function validateStoreName(storeName) {
            if (storeName.length < STORE_CONSTANTS.minNameLength ||
                storeName.length > STORE_CONSTANTS.maxNameLength) {
                throw {
                    message: STORE_CONSTANTS.invalidNameErrorMessage
                };
            }
        }

        function validateItem(item) {
            if (!item instanceof Item) {
                throw {
                    message: STORE_CONSTANTS.invalidItemTypeErrorMessage
                };
            }
        }

        return Store;
    })();

    return Store;
});