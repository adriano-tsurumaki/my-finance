'use client';

import LocaleSelect from "@components/ui/locale";
import { useTranslations } from "next-intl";

export default function Teste() {
    const t = useTranslations('home');

    return (
        <div>
            <LocaleSelect />
            <section className="max-w-xl mx-auto mt-10 p-6 bg-white rounded shadow">
                <h1 className="text-2xl font-bold mb-4">{t('title')}</h1>
                <p className="mb-2 text-gray-700">{t('description')}</p>
                <ul className="list-disc list-inside mb-4">
                    <li>{t('features.new_features')}</li>
                    <li>{t('features.updates')}</li>
                </ul>
                <div className="mt-6">
                    <a
                        href="mailto:support@example.com"
                        className="text-blue-600 underline"
                    >
                        {t('contact_us')}
                    </a>
                </div>
            </section>
        </div>
    );
}