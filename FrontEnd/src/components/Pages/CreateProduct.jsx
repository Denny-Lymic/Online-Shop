import NavBar from "../Main/Navigation";
import {
    AspectRatio, Center, HStack, Image, Textarea,
    Stack, FileUpload, Button, Input, defineStyle, Field, Box, NativeSelect,
    Text
} from "@chakra-ui/react";
import { useState } from "react";
import { HiUpload } from "react-icons/hi"
import { Form, useActionData, useLoaderData } from "react-router";

export default function CreateProduct() {
    const actionData = useActionData();
    const { categories } = useLoaderData();

    const [category, setCategory] = useState();
    const [image, setImage] = useState({
        imageSrc: null
    });

    const showPreview = e => {
        const imageFile = e.files[0];

        if (!imageFile) {
            return
        }

        const reader = new FileReader();
        reader.onload = () => {
            setImage({
                imageSrc: reader.result
            })
        }
        reader.readAsDataURL(imageFile);
    }

    return (
        <Form method="POST" encType="multipart/form-data">
            <NavBar />
            <Center mt={20}>

                <Stack w={{ base: "95vw", sm: "90vw", md: "80vw", lg: "60vw" }}>
                    <Text w="full" textAlign="center" textStyle="2xl" fontWeight="bold" mb={10}>Добавить продукт</Text>
                    <HStack>
                        <Field.Root w="60%">
                            <Box pos="relative" w="100%">
                                <Input
                                    type="text"
                                    required
                                    onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                    name="name"
                                    variant="subtle"
                                    maxLength={70}
                                    className="peer"
                                    placeholder="" />
                                <Field.Label css={floatingStyles}>Название продукта</Field.Label>
                            </Box>
                        </Field.Root>
                        <Field.Root w="20%">
                            <Box pos="relative" w="100%">
                                <Input
                                    type="number"
                                    min={0}
                                    required
                                    onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                    onInput={(e) => e.target.setCustomValidity("")}
                                    name="price"
                                    variant="subtle"
                                    maxLength={70}
                                    className="peer"
                                    placeholder="" />
                                <Field.Label css={floatingStyles}>Цена</Field.Label>
                            </Box>
                        </Field.Root>
                        <NativeSelect.Root w="20%">
                            <NativeSelect.Field
                                name="category"
                                value={category}
                                onChange={e => setCategory(e.target.value)}
                            >
                                {categories.map(category => (
                                    <option key={category.id} value={category.category}>{category.category}</option>
                                ))}

                            </NativeSelect.Field>
                            <NativeSelect.Indicator />
                        </NativeSelect.Root>
                    </HStack>
                    <HStack align="stretch">
                        <Stack w="40%">
                            <AspectRatio ratio={1}>
                                <Image
                                    borderWidth="1px"
                                    borderStyle="solid"
                                    borderColor={actionData?.errors?.image ? actionData?.imageBorderColor : "gray.300"}
                                    bg="gray.200"
                                    rounded="md"
                                    src={image.imageSrc}>
                                </Image>
                            </AspectRatio>
                            <FileUpload.Root
                                accept={["image/png", "image/jpg", "image/jpeg"]}
                                name="image"
                                onFileAccept={showPreview}
                            >
                                <FileUpload.HiddenInput />
                                <FileUpload.Trigger asChild>
                                    <Button variant="outline" size="sm" w="100%">
                                        <HiUpload /> Загрузить фотографию
                                    </Button>
                                </FileUpload.Trigger>
                            </FileUpload.Root>
                        </Stack>
                        <Textarea
                            type="text"
                            required
                            onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                            onInput={(e) => e.target.setCustomValidity("")}
                            flex="1"
                            resize="none"
                            variant="subtle"
                            name="description"
                            defaultValue={JSON.stringify(template, null, 2)}
                        />
                    </HStack>
                    <Button mt={6} size="md" type="submit">Добавить</Button>
                </Stack>
            </Center>
        </Form>
    )
}

const template = {
    tech: [
        { name: "Линейка", value: "" },
        { name: "Разъем процессора (Socket)", value: "" },
        { name: "Совместимость", value: "" },
        { name: "Количество ядер", value: "" },
        { name: "Количество потоков", value: "" },
        { name: "Частота процессора", value: "" },
        { name: "Максимальная тактовая частота", value: "" },
        { name: "Объём кэша L3", value: "" },
        { name: "Частота процессора", value: "" },
        { name: "Частота процессора", value: "" },
    ],
    kit: [
        { name: "Охлаждение в комплекте", value: "" },
        { name: "Совместимые системы охлаждения", value: "" },
        { name: "Статус процессора", value: "" },
    ]
};

const floatingStyles = defineStyle({
    pos: "absolute",
    bg: "gray.100",
    px: "0.5",
    top: "-3",
    insetStart: "2",
    fontWeight: "normal",
    pointerEvents: "none",
    transition: "position",
    _peerPlaceholderShown: {
        color: "fg.muted",
        top: "2.5",
        insetStart: "3",
    },
    _peerFocusVisible: {
        color: "fg",
        top: "-3",
        insetStart: "2",
        bg: "white",
    },
})