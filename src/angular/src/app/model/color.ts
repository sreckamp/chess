export enum Color {
  NONE = 'none',
  WHITE = 'white',
  SILVER = 'silver',
  BLACK = 'black',
  GOLD = 'gold'
}

export function parseColor(value: string): Color {
  return value && Color[value.toUpperCase()] || Color.NONE;
}
