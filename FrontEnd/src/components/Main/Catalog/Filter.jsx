import { Button, Drawer, Portal, CloseButton } from '@chakra-ui/react';

export default function Filter() {
    return (
        <Drawer.Root placement={"left"} size={"sm"}>
            <Drawer.Trigger asChild>
                <Button size="sm" ml="4">
                    Фильтр
                </Button>
            </Drawer.Trigger>
            <Portal>
                <Drawer.Backdrop />
                <Drawer.Positioner>
                    <Drawer.Content>
                        <Drawer.Header>
                            <Drawer.Title>Фильтр</Drawer.Title>
                        </Drawer.Header>
                        <Drawer.Body>
                            <p>
                                Future content goes here. You can add any components or HTML elements you like.
                            </p>
                        </Drawer.Body>
                        <Drawer.Footer>
                            <Drawer.ActionTrigger asChild>
                                <Button variant="outline">Cancel</Button>
                            </Drawer.ActionTrigger>
                            <Button>Save</Button>
                        </Drawer.Footer>
                        <Drawer.CloseTrigger asChild>
                            <CloseButton size="sm" />
                        </Drawer.CloseTrigger>
                    </Drawer.Content>
                </Drawer.Positioner>
            </Portal>
        </Drawer.Root>
    )
}