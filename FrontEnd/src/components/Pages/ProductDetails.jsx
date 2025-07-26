import { Center, HStack, Stack, AspectRatio, Image } from "@chakra-ui/react";
import { useNavigation } from "react-router";
import { useOutletContext } from "react-router";
import viteLogo from '/vite.svg'

export default function ProductDetails() {
    const { product } = useOutletContext();
    const navigation = useNavigation();
    const isBusy = navigation.state !== "idle";

    return (
        <Center>
            <Stack w={{ base: "95vw", sm: "90vw", md: "80vw", lg: "80vw" }}>
                <HStack>
                    <AspectRatio ratio={1}>
                        <Image
                            borderWidth="1px"
                            borderStyle="solid"
                            borderColor="gray.300"
                            bg="gray.200"
                            rounded="md"
                            src={viteLogo}
                        >
                        </Image>
                    </AspectRatio>
                </HStack>
            </Stack>
        </Center>
    );
}