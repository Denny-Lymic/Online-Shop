import { Slider as ChakraSlider } from "@chakra-ui/react"
import { useMemo } from "react";

export default function PriceSlider({ value: rangeStr, onChange, max = 30000 }) {
    const numericRange = useMemo(() => [
        Number(rangeStr[0]),
        Number(rangeStr[1])
    ], [rangeStr]);

    return (
        <ChakraSlider.Root
            min={0}
            max={max}
            size="md"
            onValueChange={({ value }) => onChange([String(value[0]), String(value[1])])}
            value={numericRange}
        >
            <ChakraSlider.Control>
                <ChakraSlider.Track>
                    <ChakraSlider.Range />
                </ChakraSlider.Track>
                <ChakraSlider.Thumbs />
            </ChakraSlider.Control>
        </ChakraSlider.Root >
    )
}
