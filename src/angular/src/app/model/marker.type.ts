// noinspection JSUnusedGlobalSymbols
export enum MarkerType {
    NONE = 'none',
    COVER = 'cover',
    CHECK = 'check',
    ENPASSANT = 'enpassant'
}

export function parseMarkerType(value: string): MarkerType {
    return value && MarkerType[value.toUpperCase()] || MarkerType.NONE;
}
