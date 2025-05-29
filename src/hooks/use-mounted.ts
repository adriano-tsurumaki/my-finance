import React from 'react';

/**
 * React hook to determine if a component has been mounted.
 *
 * Useful for avoiding hydration issues in Next.js applications by ensuring
 * that certain components or logic only run after the component is mounted
 * on the client side. This is particularly important when working with
 * libraries that manipulate the DOM or require browser-specific APIs that
 * are not available during server-side rendering.
 *
 * @returns {boolean} `true` if the component has been mounted, otherwise `false`.
 *
 * @example
 * const mounted = useMounted();
 * if (!mounted) {
 *   return null; // Prevents rendering before the component is mounted
 * }
 */
export default function useMounted(): boolean {
    const [mounted, setMounted] = React.useState(false);

    React.useEffect(() => {
        setMounted(true);
    }, []);

    return mounted;
}