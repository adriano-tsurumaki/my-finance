'use client';

import SunMoonToggle from "@components/ui/sun-moon-toogle";
import LocaleSelect from "@components/ui/locale";
import { useTranslations } from "next-intl";

export default function Teste() {
    const t = useTranslations('home');

    return (
        <div className="bg-white dark:bg-gray-900 transition-colors duration-300">
            <div className="flex gap-2">
                <LocaleSelect />
                <SunMoonToggle />
            </div>
            <section className="max-w-xl mx-auto mt-10 p-6 bg-white dark:bg-gray-800 rounded shadow transition-colors duration-300">
            <h1 className="text-2xl font-bold mb-4 text-gray-900 dark:text-white">{t('title')}</h1>
            <p className="mb-2 text-gray-700 dark:text-gray-300">{t('description')}</p>
            <ul className="list-disc list-inside mb-4 text-gray-700 dark:text-gray-300">
                <li>{t('features.new_features')}</li>
                <li>{t('features.updates')}</li>
            </ul>
            <div className="mt-6">
                <a
                href="mailto:support@example.com"
                className="text-blue-600 dark:text-blue-400 underline"
                >
                {t('contact_us')}
                </a>
            </div>
            </section>
        </div>
    );
}