export class Point {
    constructor(
        public readonly x: number = 0,
        public readonly y: number = 0) {
    }
}

export class Placement<T> {
    public readonly location: Point;

    constructor(x: number, y: number, public readonly value: T) {
        this.location = new Point(x, y);
    }
}
