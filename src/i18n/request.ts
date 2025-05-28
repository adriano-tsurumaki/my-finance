import { loadMessages } from '@/lib/loadMessages';
import { getUserLocale } from '@/lib/locale';
import { getRequestConfig } from 'next-intl/server';
import { cookies } from 'next/headers';

export default getRequestConfig(async () => {
    const locale = await getUserLocale();

    const messages = await loadMessages(locale);

    return {
        locale,
        messages,
        cookies: cookies(),
    }
});