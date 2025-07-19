import { Avatar as ChakraAvatar } from '@chakra-ui/react';

export default function Avatar({ src, fallbackName }) {
    return (
        <ChakraAvatar.Root>
            <ChakraAvatar.Image src={src} size="sm" />
            <ChakraAvatar.Fallback name={fallbackName} />
        </ChakraAvatar.Root>
    );
}