export type Locale = (typeof locales)[number];

export const locales = ['en', 'pt-BR'] as const;
export const defaultLocale: Locale = 'en';