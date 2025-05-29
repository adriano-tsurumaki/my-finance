'use client';

import { motion, AnimatePresence } from "motion/react";
import { Button } from "./button";
import { useTheme } from "next-themes";
import { useEffect, useState } from "react";
import useMounted from "@hooks/use-mounted";

export default function SunMoonToggle() {
    const { setTheme, theme } = useTheme();
    const [isDark, setIsDark] = useState(theme === "dark");
    const mounted = useMounted();

    useEffect(() => {
        setIsDark(theme === "dark");
    }, [theme])

    if (!mounted) {
        return null; // Evita renderização antes do componente ser montado
    }

    return (
        <Button onClick={() => {
            setTheme(isDark ? "light" : "dark");
        }}
            className="size-10 bg-[#f9d71c] dark:bg-[#805AD5] hover:bg-[#e6c200] dark:hover:bg-[#805AD5b7] cursor-pointer"
        >
            <AnimatePresence>
                <svg className="size-8" width="100" height="100" viewBox="0 0 100 100">
                    <defs>
                        <motion.mask id="moon-mask">
                            <rect width="100%" height="100%" fill="white" />
                            <motion.circle
                                cx={60}
                                cy={40}
                                r={isDark ? 20 : 0.001} // usa 0.001 no lugar de 0 para permitir interpolação suave
                                fill="black"
                                animate={{
                                    cx: isDark ? 60 : 80,
                                    cy: isDark ? 40 : 20,
                                    r: isDark ? 20 : 0.001,
                                }}
                                transition={{ duration: isDark ? 0.3 : 0.5, delay: isDark ? 0.1 : 0, ease: "easeInOut" }}
                            />
                        </motion.mask>
                    </defs>
                    {/* Sol com máscara aplicada */}
                    <motion.circle
                        cx="50"
                        cy="50"
                        r="20"
                        mask={"url(#moon-mask)"}
                        animate={{
                            fill: isDark ? "#fff" : "#333"
                        }}
                        transition={{ duration: 0.5 }}
                    />

                    {/* Raios do sol */}
                    {[...Array(8)].map((_, i) => {
                        const angle = (i * 360) / 8;
                        const innerRadius = 26;
                        const outerRadius = 36;
                        const rad = (angle * Math.PI) / 180;

                        const x1 = 50 + Math.cos(rad) * innerRadius;
                        const y1 = 50 + Math.sin(rad) * innerRadius;
                        const x2 = 50 + Math.cos(rad) * outerRadius;
                        const y2 = 50 + Math.sin(rad) * outerRadius;

                        return (
                            <motion.line
                                key={i}
                                x1={x1}
                                y1={y1}
                                x2={isDark ? x1 : x2}
                                y2={isDark ? y1 : y2}
                                stroke="#333"
                                strokeWidth="3"
                                initial={false}
                                animate={{
                                    x2: isDark ? x1 : x2,
                                    y2: isDark ? y1 : y2,
                                }}
                                transition={{ duration: 0.25, delay: isDark ? 0 : 0.2 }}
                            />
                        );
                    })}
                </svg>
            </AnimatePresence>
        </Button>
    );
}
