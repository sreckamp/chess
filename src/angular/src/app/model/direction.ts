// noinspection JSUnusedGlobalSymbols
export enum Direction {
    NONE = 'none',
    NORTH = 'north',
    NORTHEAST = 'northeast',
    EAST = 'east',
    SOUTHEAST = 'southeast',
    SOUTH = 'south',
    SOUTHWEST = 'southwest',
    WEST = 'west',
    NORTHWEST = 'northwest',
}

export function parseDirection(value: string): Direction {
    return value && Direction[value.toUpperCase()] || Direction.NONE;
}
