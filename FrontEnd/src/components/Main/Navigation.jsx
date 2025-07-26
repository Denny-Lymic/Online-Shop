import { Link } from 'react-router'
import User from './UserBox/User'
import { Flex, Text, Spacer, Drawer, Portal, Button, CloseButton, Stack, LinkBox, LinkOverlay } from '@chakra-ui/react'

export default function NavBar() {
    return (
        <>
            <Flex pl={8} bg="gray.100" align="center" shadow="lg" hideBelow="md">
                <Text fontSize="xl" fontWeight="bold" py={4}>Online-Shop</Text>

                <Flex gap={2} ml={6} align={"stretch"}>
                    <LinkBox><LinkOverlay as={Link} to="/" py={4} px={2}>Главная</LinkOverlay></LinkBox>
                    <LinkBox><LinkOverlay as={Link} to="/create" py={4} px={2}>Добавить продукт</LinkOverlay></LinkBox>
                </Flex>

                <Spacer />

                <User />
            </Flex>

            <Drawer.Root placement={"left"} size={"sm"}>
                <Drawer.Trigger asChild>
                    <Button size="sm" ml="4" hideFrom="md" my="2">
                        "Бургер меню"
                    </Button>
                </Drawer.Trigger>
                <Portal>
                    <Drawer.Backdrop />
                    <Drawer.Positioner>
                        <Drawer.Content>
                            <Drawer.Header>
                                <Drawer.Title><Text fontSize="xl" fontWeight="bold">Online-Shop</Text></Drawer.Title>
                            </Drawer.Header>
                            <Drawer.Body>
                                <Stack>
                                    <Link href="#">Главная</Link>
                                    <Link href="#">Каталог</Link>
                                    <Link href="#">О нас</Link>
                                </Stack>
                            </Drawer.Body>
                            <Drawer.Footer>
                                <Drawer.ActionTrigger asChild>
                                    <Button variant="outline">Cancel</Button>
                                </Drawer.ActionTrigger>
                            </Drawer.Footer>

                            <Drawer.CloseTrigger asChild>
                                <CloseButton size="md" />
                            </Drawer.CloseTrigger>
                        </Drawer.Content>
                    </Drawer.Positioner>
                </Portal>
            </Drawer.Root>
        </>
    )
}
