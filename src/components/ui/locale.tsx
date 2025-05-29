"use client";

import { startTransition } from "react";
import { useTranslations, useLocale } from "next-intl";
import { Select, SelectTrigger, SelectContent, SelectItem, SelectValue } from "@components/ui/select";
import { Locale } from '@i18n/config';
import { setUserLocale } from "@lib/locale";

const locales = [
    { code: "en", label: "English" },
    { code: "pt-BR", label: "Português" },
];

export default function LocaleSelect() {
    const t = useTranslations("settings.locale");
    const locale = useLocale();

    const handleChange = (value: string) => {
        const locale = value as Locale;
        startTransition(() => {
            setUserLocale(locale);
        });
    };

    return (
        <Select value={locale} onValueChange={handleChange}>
            <SelectTrigger className="w-[120px]">
                <SelectValue placeholder={t("placeholder")} />
            </SelectTrigger>
            <SelectContent>
                {locales.map((l) => (
                    <SelectItem key={l.code} value={l.code}>
                        {l.label}
                    </SelectItem>
                ))}
            </SelectContent>
        </Select>
    );
}