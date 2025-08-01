import { Center, Stack, AspectRatio, Image, Card, Button, Text, Span, Box, Separator } from "@chakra-ui/react";
import { Await, Link, useNavigation } from "react-router";
import { useOutletContext } from "react-router";
import viteLogo from "/vite.svg"
import { Suspense } from "react";
import React from "react";

export default function ProductDetails() {
    const { product } = useOutletContext();
    const navigation = useNavigation();
    const isBusy = navigation.state !== "idle";

    return (
        <Suspense>
            <Await resolve={product}>
                {(data) => (
                    <Center mt={10}>
                        <Stack w={{ base: "90vw", lg: "60vw" }}>
                            <Stack w="100%" direction={{ base: "column", md: "row" }} alignItems="flex-start">
                                <AspectRatio
                                    ratio={1}
                                    w={{ base: "100%", md: "60%", lg: "50%" }}
                                >
                                    <Image
                                        borderWidth="1px"
                                        borderStyle="solid"
                                        borderColor="gray.300"
                                        rounded="md"
                                        src={data.imageUrl ? `${import.meta.env.VITE_API_URL}/images/products/${data.imageUrl}` : viteLogo}
                                        w="100%"
                                    />

                                </AspectRatio>
                                <Card.Root
                                    alignItems="center" textAlign="center"
                                    w={{ base: "100%", md: "40%", lg: "50%" }}
                                    position={{ base: "static", md: "sticky" }}
                                    top="10px"
                                    px={0}
                                >
                                    <Card.Header px={0}>
                                        <Text fontSize="2rem" fontWeight="semibold" mb={3}>{data.name}</Text>
                                        <Text fontSize="2rem" fontWeight="medium">{data.price} <Span fontSize="0.8em">₴</Span></Text>
                                    </Card.Header>
                                    <Card.Footer mt={10} px={0} w="90%">
                                        <Stack w="100%">
                                            <Stack direction={{ base: "column", md: "column", lg: "row" }}>
                                                <Button
                                                    fontSize="xl"
                                                    fontWeight="bold"
                                                    w={{ base: "100%", md: "100%", lg: "39%" }}
                                                    py={7}
                                                    rounded="4xl"
                                                    borderRadius={80}
                                                    transition="all 0.2s ease"
                                                    bg="rgb(148, 184, 10)"
                                                    _hover={{
                                                        bg: "rgb(122, 151, 9)"
                                                    }}
                                                >
                                                    Купить
                                                </Button>
                                                <Button
                                                    fontSize="xl"
                                                    fontWeight="bold"
                                                    w={{ base: "100%", md: "100%", lg: "59%" }}
                                                    py={7}
                                                    rounded="4xl"
                                                    borderRadius={80}
                                                    transition="all 0.2s ease"
                                                    bg="rgb(148, 184, 10)"
                                                    _hover={{
                                                        bg: "rgb(122, 151, 9)"
                                                    }}
                                                >
                                                    Добавить в корзину
                                                </Button>
                                            </Stack>
                                            <Stack direction={{ base: "column", md: "column", lg: "row" }} mt={1}>
                                                <Button
                                                    fontSize="xl"
                                                    fontWeight="bold"
                                                    w={{ base: "100%", md: "100%", lg: "49%" }}
                                                    py={7}
                                                    rounded="4xl"
                                                    borderRadius={80}
                                                    transition="all 0.2s ease"
                                                    bg="rgb(148, 184, 10)"
                                                    _hover={{
                                                        bg: "rgb(122, 151, 9)"
                                                    }}
                                                    as={Link}
                                                    to={`update`}
                                                >
                                                    Изменить
                                                </Button>
                                                <Button
                                                    fontSize="xl"
                                                    fontWeight="bold"
                                                    w={{ base: "100%", md: "100%", lg: "49%" }}
                                                    py={7}
                                                    rounded="4xl"
                                                    borderRadius={80}
                                                    transition="all 0.2s ease"
                                                    bg="rgb(148, 184, 10)"
                                                    _hover={{
                                                        bg: "rgb(122, 151, 9)"
                                                    }}
                                                >
                                                    Удалить
                                                </Button>
                                            </Stack>
                                        </Stack>
                                    </Card.Footer>
                                </Card.Root>
                            </Stack>
                            {
                                data.description && Object.entries(JSON.parse(data.description)).map(([category, items]) => (
                                    <Stack key={category}>
                                        <Text fontWeight="semibold" fontSize="xl" pb={5} mt={10}>{category}</Text>

                                        {items.map(item => (
                                            <React.Fragment key={item.name}>
                                                <Stack direction={{ base: "column", md: "column", lg: "row" }} my={2}>
                                                    <Box minW={400} fontWeight="500">{item.name}</Box>
                                                    <Box flex="1">{item.value}</Box>
                                                </Stack>
                                                <Separator />
                                            </React.Fragment>
                                        ))}
                                    </Stack>
                                ))
                            }
                        </Stack>
                    </Center>
                )}
            </Await>
        </Suspense>
    );
}