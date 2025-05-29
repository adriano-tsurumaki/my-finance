import fs from 'fs';
import path from 'path';

export async function loadMessages(locale: string) {
  const localePath = path.join(process.cwd(), 'src/messages', locale);
  const files = fs.readdirSync(localePath);

  const messages = files.reduce((acc, file) => {
    const key = path.basename(file, '.json');
    const content = JSON.parse(
      fs.readFileSync(path.join(localePath, file), 'utf-8')
    );
    acc[key] = content;
    return acc;
  }, {} as Record<string, any>);

  return messages;
}