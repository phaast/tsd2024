class RandomCollection<T> {
    private items: T[] = [];

    add(element: T): void {
        if (Math.random() < 0.5) {
            this.items.unshift(element);
        } else {
            this.items.push(element);
        }
    }

    get(index: number): T {
        if (this.isEmpty()) {
            throw new Error("Collection is empty.");
        }
        if (index < 0 || index >= this.items.length) {
            throw new Error("Index out of bounds.");
        }

        const randomIndex = Math.floor(Math.random() * (index + 1));
        return this.items[randomIndex];
    }

    isEmpty(): boolean {
        return this.items.length === 0;
    }

    printAll(): void {
        console.log(`Collection: [${this.items.join(", ")}]`);
    }
}

console.log("--- Task 2: TypeScript RandomCollection ---");
const collection = new RandomCollection<string>();

console.log(`Is the collection initially empty? ${collection.isEmpty()}`);

collection.add("Adam");
collection.add("Milosz");
collection.add("Aleks");
collection.add("Kinga");

collection.printAll();

console.log(`Random element from indices 0-2: ${collection.get(2)}`);
console.log(`Is the collection empty? ${collection.isEmpty()}`);